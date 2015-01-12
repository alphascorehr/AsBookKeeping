/* 
 * Auto Expanding Text Area (1.2.2)
 * by Chrys Bader (www.chrysbader.com)
 * chrysb@gmail.com
 *
 * Special thanks to:
 * Jake Chapa - jake@hybridstudio.com
 * John Resig - jeresig@gmail.com
 *
 * Copyright (c) 2008 Chrys Bader (www.chrysbader.com)
 * Licensed under the GPL (GPL-LICENSE.txt) license. 
 *
 */
(function(jQuery) {

	var _this,
		dummy,
		uid, // counter for created autogrow boxes
		lastUid; // id of last autogrow box used.  Used to recheck boxes with similar content, but different boxes.

	jQuery.fn.autogrow = function(o) {
		return this.each(function() {
			new jQuery.autogrow(this, o);
		});
	};

    /**
     * The autogrow object.
     *
     * @constructor
     * @name jQuery.autogrow
     * @param Object e The textarea to create the autogrow for.
     * @param Hash o A set of key/value pairs to set as configuration properties.
     * @cat Plugins/autogrow
     */
	jQuery.autogrow = function (e, o) {
		var el = jQuery(e);
		this.options = o || {};
		this.font_size = this.options.fontSize;
		this.font_family = this.options.fontFamily;
		this.width = this.options.width;
		this.line_height = this.options.lineHeight || parseInt(el.css('line-height'));
		this.letter_spacing = this.options.letterSpacing || el.css('letter-spacing');
		this.min_height = this.options.minHeight || parseInt(el.css('min-height'));
		this.max_height = this.options.maxHeight || parseInt(el.css('max-height'));
		
		this.textarea = el;
		this.callback = this.options.callback;

		if (isNaN(this.line_height)) {
			this.line_height = 0;
		}
		uid += 1;
		this.uid = uid;

		// Only one textarea activated at a time, the one being used
		this.init();
	};
	
	jQuery.autogrow.fn = jQuery.autogrow.prototype = {
		autogrow: '1.2.2'
	};
	
 	jQuery.autogrow.fn.extend = jQuery.autogrow.extend = jQuery.extend;
	
	jQuery.autogrow.fn.extend({

		init: function() {
			var _this = this;
			this.textarea.css({overflow: 'hidden', display: 'block'});
			this.textarea.bind('change', function () {
				_this.checkExpand();
			});
			var timeout;
			this.textarea.bind('paste', function (event) {
				clearTimeout(timeout);
				timeout = setTimeout(function () {
					var dummyHeight = dummy.height();
					_this.textarea.scrollTop(dummyHeight);
					_this.checkExpand();
				}, 15);
			}).bind('keyup', function () {
				_this.checkExpand();
			});
			this.checkExpand();
		},

		checkExpand: function () {
			if (!dummy) {
				dummy = jQuery('<div id="autogrow"></div>');
				dummy.css({
					'font-size': this.font_size,
					'font-family': this.font_family,
					'width': this.width,
					'padding': '0px',
					'line-height': this.line_height + 'px',
					'letter-spacing': this.letter_spacing,
					'overflow-x': 'hidden',
					'position': 'absolute',
					'top': 0,
					'left': -9999
				}).appendTo('body');
			}

			// Strip HTML tags
			var html = this.textarea.val().replace(/(<|>)/g, '');

			// IE is different, as per usual
			if ($.browser.msie) {
				html = html.replace(/\n/g, '<BR>new');
			} else if ($.browser.safari) {
				html = html.replace(/\n$/, '').replace(/\n/g, '<br>new');
			} else {
				html = html.replace(/\n/g, '<br>new');
			}
			
			// Convert line spaces - need to convert "pairs" of spaces so
			// that we still get breaking on lines
			html = html.replace(/  /g, ' &nbsp;');

			if (dummy.html() != html || lastUid != this.uid) {
				lastUid = this.uid;
				dummy.html(html);
				// Jigger the height around and cheat an extra line in once we get to 3 lines.
				// this fixes text wrapping inconistencies between divs + textareas.
				var dummyHeight = Math.max(dummy.height(), this.min_height);
				dummyHeight += (dummyHeight > this.line_height * 2) ? this.line_height : 0;

				if (this.max_height > 0 && (dummyHeight > this.max_height)) {
					this.textarea.height(this.max_height);
					this.textarea.css('overflow-y', 'auto');
				} else {
					this.textarea.css('overflow-y', 'hidden');
					this.textarea.height(dummyHeight + 'px');
					if (this.callback) {
						this.callback(dummyHeight);
					}
				}
			}
		}
	 });
})(jQuery);