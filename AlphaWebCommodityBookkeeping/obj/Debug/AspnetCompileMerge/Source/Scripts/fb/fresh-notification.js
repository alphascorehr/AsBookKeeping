/**
 * FreshBooks 2010
 * 
 * FreshNotification is a system for handing out onetime notices for users of FreshBooks,
 * as a result of various thresholds and events being triggered in their system. 
 *
 */

var FreshNotification = FreshBadge.extend({
	checkEndpoint: '/ajax/freshNotification/check',
	finishEndpoint: '/ajax/freshNotification/finish',
	
	finish: function(possibleNotification) {
		var self = this;

		$.post(
			self.finishEndpoint,
			{
				notification: possibleNotification
			}
		);
	}
});

var FreshUserNotification = FreshNotification.extend({
	checkEndpoint: '/ajax/freshUserNotification/check',
	finishEndpoint: '/ajax/freshUserNotification/finish'
});