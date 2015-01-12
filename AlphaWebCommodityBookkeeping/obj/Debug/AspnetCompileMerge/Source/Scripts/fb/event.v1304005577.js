/**
 * FreshBooks 2010
 *
 * This plugin allows for events to be logged to events_ng via JavaScript. the types of events logged in this way, 
 * and the duration for which information is collected should be thought through thoroughly 
 * -- our events table is already bloated.
 */

(function ($) {
	$.fn.logClickEvent = function(eventName, objectID) {
		var $eventElement = $(this);
		
		// Store the event data to the event element.
		$eventElement.data('eventName', eventName);
		$eventElement.data('objectID', objectID);
		
		 // Fallback for elements created on the fly, e.g., lightboxes.
		var liveEventLookup = {};		
		if ($eventElement.attr('id')) {
			liveEventLookup[$eventElement.attr('id')] = {
				eventName: eventName,
				objectID: objectID
			};
		}
		
		var clickEvent = function(event) {
			var $eventElement = $(this);
			var eventName = $eventElement.data('eventName');
			var objectID = $eventElement.data('objectID');
							
			if (!eventName) {
				eventName = liveEventLookup[$eventElement.attr('id')]['eventName'];
				objectID = liveEventLookup[$eventElement.attr('id')]['objectID'];
			}
							
			$.ajax({	
				url: '/ajax/event/logEvent',
				data: {
					eventName: eventName,
					objectID: objectID
				},
				dataType: 'json',
				type: 'POST',
				success: function (data) {
				}
			});
			
			return true;
		};
		
		// Actually bind the event.
		$eventElement.live('click', clickEvent);
	}
})(jQuery);