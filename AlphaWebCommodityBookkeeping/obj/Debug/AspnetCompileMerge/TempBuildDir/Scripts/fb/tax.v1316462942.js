/*
Tax editing and UI control widgets.

Creates the multi-row form for working with tax sets.

Fresh.Tax works fine for standard HTML forms. See below for an XHR-backed version.
*/
Fresh.Tax = Fresh.ui.Widget.extend({

	defaults: {
		element: null,
		template: null,
		insertionPoint: null,
		addButton: '#add-new-tax',
		deleteButton: '.cross-button',
		rowClass: '.tax_row',
		compoundColumn: '.tax_compound',
		autoLoad: false,
		fields: {
			taxid:    '.tax_id input',
			name:     '.tax_name input',
			amount:   '.tax_amount input',
			number:   '.tax_number input',
			deleted:  '.tax_delete input',
			compound: '.tax_compound input'
		},
		taxes: [],
		leftTaxIds: [],
		mapper: null,
		compound: null
	},

	name: 'tax',
	element: null,
	template: null,
	
	// Inputs are named 'taxes[{count}][key] where count is
	// the index of the row, and key is something like 'name' or 'amount'
	// If we don't manually fill in count, then PHP doesn't interpret the
	// array indices properly.
	count: 0, 

	init: function (options) {
		this.options = jQuery.extend({}, this.defaults, options);
		this._super(this.options);

		this.element = $(this.options.element);
		this.template = Fresh.Util.getTemplate(this.options.template, { default_formatter: 'html-attr-value' });
		this.addButton = $(this.options.addButton);
		this.insertionPoint = $(this.options.insertionPoint);
		this._bindEvents();
		this._display();
	},

	_bindEvents: function () {
		var _this = this;
		this.addButton.live('click', function (event) {
			_this.addRow();
			_this.focus();
			return false;
		});
		
		this.element.find(this.options.deleteButton).live('click', function (event) {
			$(this).parents(_this.options.rowClass).fadeOut('fast', function () {
				// We have to hide the row and set this flag so the server knows
				// which records to delete and which to update. It's too much work
				// to try and manually figure it out server-side.
				$(this).find(_this.options.fields.deleted).attr('value', 1);
			});
			return false;
		});
	},
	
	focus: function () {
		var $errors = this.element.find(this.options.rowClass + ' .errorInput:first');

		if ($errors.length) {
			$errors.focus();
		} else {
			this.element.find(this.options.rowClass + ':last input:visible:first').focus();
		}
	},
	
	addRow: function (tax) {
		tax = $.extend({}, {taxid: null, name: '', number: '', amount: '', errors: [], compound: 0}, tax);
		tax.disabled = $.inArray(Number(tax.taxid), this.options.leftTaxIds) !== -1 ? true : false;
		
		var templateContent = $(this.template.expand({
			taxid:  tax.taxid,
			name:   tax.name,
			amount: tax.amount,
			number: tax.number,
			count:  this.count,
			compound: tax.compound,
			disabled: tax.disabled
		}));
		
		if (tax.errors) {
			for (var i = 0; i < tax.errors.length; i++) {
				$('input[name*=' + tax.errors[i] + ']', templateContent).addClass('errorInput');
			}
		}
		
		if (tax.disabled) {
			templateContent.find(this.options.compoundColumn).tooltip({
				extraClass: 'tax-tooltip'
			});
		}
		
		// Hide compound taxes checkbox if it's not in use
		if (!this.options.compound) {
			templateContent.find(this.options.compoundColumn).hide();
		}
		
		this.insertionPoint.before(templateContent);
		this.count++;
	},
	
	_display: function() {
		var _this = this;
		
		// Hide the compound tax header
		if (!this.options.compound) {
			this.element.find(this.options.compoundColumn).hide();
		}
		
		// First remove any pre-existing table rows
		this.element.find(this.options.rowClass).remove();
		this.count = 0;
		
		// Ensure form submit won't work
		this.element.find('form').bind('submit', function() {
			return false;
		});
		
		var taxes  = this.options.taxes;
		
		// add existing rows.
		for (var i = 0; i < taxes.length; i++) {
			if (!taxes[i].taxid && !taxes[i].name && !taxes[i].amount && !taxes[i].number) {
				continue;
			}
			this.addRow(taxes[i]);
		}
		
		// add a blank one
		this.addRow();
		this.focus();
	}
});

/*
Create an XHR backed form slice used on the invoice-y pages.
*/
Fresh.Tax.InvoicePanel = Fresh.Tax.extend({

	defaults: {
		cancelButton: '#cancel-taxespop',
		loading: '#tax_loading',
		submit: '#tax_submit',
		errors: '#tax_errors',
		saveButton: '#save-taxespop'
	},
	
	init: function (options) {
		options = jQuery.extend({}, this.defaults, Fresh.Tax.prototype.defaults, options);
		this._super(options);
		
		this.errors = $(this.options.errors);
		this.submitBlock = $(this.options.submit);
		this.loading = $(this.options.loading);
	},

	_bindEvents: function () {
		var _this = this;
		this._super();
		$(document).bind('invoice.editTaxes', function () {
			_this.displayPanel();
		});

		$(this.options.cancelButton).live('click', function (event) {
			$(document).trigger('close.facebox');
			return false;
		});

		$(this.options.saveButton).live('click', function (event) {
			_this.save();
			return false;
		});
	},

	displayPanel: function () {

		this.errors.empty(); // clear out any old errors
		this.loading.hide(); // hide the loading img
		this.submitBlock.show();  // reveal the submit buttons

		// launch the lightbox/thickbox
		$.facebox({ div: this.element.selector });
		
		this._display();
		this.focus();
	},
	
	_buildMapper: function () {
		
		var fields = {},
			_this  = this;
		
		// The fields are set up a bit wonky so that we have them in 
		// a nice hash table when we send it over the wire. Setting
		// them up this way clashes a bit with the mapper though.
		$(this.options.rowClass + ' input').each(function() {
			fields[this.name] = _this.options.rowClass + ' input[name="' + this.name + '"]';
		});
		
		this.mapper = new Fresh.ui.FieldMapper({
			fields: fields
		});
	},

	save: function () {
		
		this._buildMapper();
		
		this.errors.empty();
		this.submitBlock.hide();
		this.loading.show();

		this._super();
	},

	saveSuccess: function (response) {
		this.options.taxes = response.taxes;

		// Close the popup - do this first, because while the animation
		// is running, we can run the rest of our script
		$(document).trigger('close.facebox');

		invoice.updateTaxes(this.options.taxes);
	},

	saveError: function (request) {
		var json = JSON.parse(request.responseText);
		
		for (var i = 0; i < json.errors.length; i++) {
			this.errors.append('<li><span>' + json.errors[i].htmlentities() + '</span></li>');
		}
		
		// Update the taxes displayed with the ones that have been processed by the form
		this.options.taxes = json.taxes;
		this._display();
		
		this.loading.hide();
		this.submitBlock.show();
	}
});
