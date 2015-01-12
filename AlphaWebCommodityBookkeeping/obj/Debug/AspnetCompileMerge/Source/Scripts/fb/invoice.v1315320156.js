Fresh.ui.InvoiceGuiders = Class.extend({
	defaults: {},
	
	init: function (options) {
		var self = this;
		
		this.options = $.extend({}, this.defaults, options || {});
		this.noClients = this.options.noClients;

		this.guidedTasks = new Fresh.ui.GuidedTask.List(this.tasks());
		
		$(document).bind('beforeShow.sendInvoicePopup', function (e) {
			self.guidedTasks.getTaskByName('line-items').markAsDone();
		});
		
		$(document).bind('show.logoUploader', function () {
			guiders.hideAll();
			self.guidedTasks.showingTask = null;
		});
		
		$(document).bind('hide.logoUploader', function () {
			self.guidedTasks.showNextTask();
		});
	},
	
	start: function () {
		if (this.noClients) {
			$('#customerid').val('NEW').trigger('change');
		}
		this.guidedTasks.showNextTask();
	},
	
	tasks: function () {
		var tasks = [
			{
				id: "client",
				attachTo: '#client_section',
				position: 11,
				buttons: [],
				title: "Every invoice needs a client!",
				description: "<b>Every invoice starts with a client</b> Choose or create a new client from this here drop down menu.",
				width: 420,
				next: "line-items",
				init: function(task) {
					task.dispatch.one('invoice.refreshClient', function () {
						task.hideHelp();
						
						setTimeout(function () {
							task.markAsDone();
						}, 2000);
					});
				}
			},
			{
				id: "line-items",
				attachTo: "#invoice-lines",
				position: 11,
				buttons: [],
				title: "Describe your work",
				description: "<b>What work are you billing for?</b> Enter your billable hours or flat fee items.",
				width: 420,
				next: 'terms-conditions',
				init: function (task) {
					task.dispatch.bind('fb.invoice_base.update_totals', function (e, data) {
						if (data.total > 0) {
							task.hideHelp();
							
							if (task.delayDone) {
								clearTimeout(task.delayDone);
								task.delayDone = null;
							}
							
							task.delayDone = setTimeout(function () {
								task.dispatch.unbind('fb.invoice_base.update_totals');
								task.markAsDone();
							}, 7000);							
						}
					});
				}
			},
			{
				id: "terms-conditions",
				attachTo: "label[for=terms]",
				position: 11,
				buttons: [],
				title: "Spell out your terms",
				description: "<b>Eliminate confusion.</b> Politely inform your clients of when and how you like to get paid.",
				width: 420,
				next: 'send',
				init: function (task) {
					var terms = $('#terms');
					
					terms.one('click', function (e) {
						var autoNextGuider;
						
						task.hideHelp();
											
						terms.one('blur', function (e) {
							task.markAsDone();
							terms.unbind('keypress');
						});						

						terms.bind('keypress', function (e) {
							if (autoNextGuider) {
								clearTimeout(autoNextGuider);
								autoNextGuider = null;
							}
							 
							autoNextGuider = setTimeout(function() {
								terms.unbind('keypress');
								task.markAsDone();
								terms.blur();
							}, 4000);
						});
					});
					
					// Chromium <14: doesn't support wrapping and next lines
					// Firefox 5: doesn't support next lines
					terms.attr('placeholder', "Sample terms: \nThank you for your business. Please send payment via Paypal or check within 30 days of receiving this invoice.");
				}
			},
			{
				id: "send",
				attachTo: "#send_by_email_button",
				position: 1,
				buttons: [],
				title: "Send your Invoice",
				description: "<b>Getting paid is within reach!</b> Click to write an email that will accompany your invoice.",
				width: 420,
				offset: {
					top: 0,
					left: -40
				},
				init: function (task) {
					$(document).bind('beforeShow.sendInvoicePopup', function () {
						task.markAsDone();
					});
				}
			}
		];
		
		if (this.noClients) {
			var task = {
				id: "client",
				attachTo: '#edit_client_box',
				position: 11,
				buttons: [],
				title: "Time to get paid",
				description: "<b>Which client do you need to bill?</b> Enter your client's essentials for your invoice.",
				width: 420,
				next: "line-items",
				init: function(task) {
					task.dispatch.one('invoice.refreshClient', function () {
						setTimeout(function () {
							task.markAsDone();
						}, 2000);
					});
				}
			};

			tasks.shift();			
			tasks.unshift(task);
		}
		return tasks;
	}
});