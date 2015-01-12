/**
 * FreshBooks 2010
 * Benjamin E. Coe
 * 
 * FreshBadge is a system for handing out badges to users of FreshBooks,
 * as a result of various thresholds and events being triggered in their system. The idea is to:
 *
 * 1) Appeal to peoples' competitive nature
 * 2) Draw attention to parts of FreshBooks that are not frequently used, e.g., referrals.
 */

var FreshBadge = Class.extend({
	checkEndpoint: '/ajax/freshBadge/check',
	
	init: function(params) {
		params = params || {};
		this.$badgeArea = params.badgeArea || '';
		this.$messageArea = params.messageArea || '';
		this.$contentArea = params.contentArea || '';
		this.$tipArea = params.tipArea || '';
		this.referralLink = params.referralLink || '';
		this.twitterURL = params.twitterURL || '';
		this.closeLink = params.closeLink || '';
		this.callbacks = params.callbacks || {};
	},
	
	ensureMessageArea: function() {
		if (this.$messageArea.length == 0) {
			
			var $messageBox = $('<div class="notifyBox "><h3>Well Done!</h3></div>');
			var $messageArea = $('<ul></ul>');
			$messageBox.append($messageArea);

			this.$contentArea.prepend($messageBox);
			this.$messageArea = $messageArea;
		}
	},
	
	bindClose: function() {
		var self = this;
		
		// Attach a close event to an arbitrary link.
		if (self.closeLink) {
			self.$messageArea.find('.' + self.closeLink).click(function(e) {
				self.$messageArea.hide();
				e.stopPropagation();
				e.preventDefault();
			});
		}
	},
	
	check: function(possibleBadges) {
		var self = this;
		
		// Should we restrict which badge types
		// are checked.
		var data = {};
		if (possibleBadges) {
			data = {badges: possibleBadges};
		}
		
		$.ajax({
			url: self.checkEndpoint,
			data: data,
			dataType: 'json',
			type: 'GET',
			success: function (data) {	
				 // Were any events returned?
				if (data.events) {
					for (var i = 0; i < data.events.length; i++) {
						var badgeData = data.events[i];
						if (badgeData.selector && badgeData.content) {
							$(badgeData.selector).html(badgeData.content);
							self.bindClose();
						} else if (self.callbacks[badgeData.type]) {
							self.callbacks[badgeData.type].call(self, badgeData);
						}
					}
				}
			}
		});
	}
});

/*
 * Utility method used for account checklist badge display.
 */
FreshBadge.displayHeaderMessage = function (badge) {
	var img = new Image();
	img.src = badge.image + '?timestamp=' +new Date();
	img.className = 'checklist-reward';
	$('.logo-container').append(img);
}
