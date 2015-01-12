/**
 * FreshBooks 2010
 * 
 * A set of helper functions for interactin third-party services. e.g.,
 * bit.ly. To better explain, I've started the design pattern of putting
 * non-mission-critical functionality that relies on third-party services
 * behind AJAX calls -- this allows us to gracefully fail if the service, e.g.,
 * bit.ly fails.
 *
 * The corresponding controller can be found in inc/ajax/controllers/ExternalAPIAjaxController.class.php
 */
 
/**
 * Given a URL DOM element replaces it with the bit.ly short-form.
 * or can be passed a URL and bit.ly encodes it.
 */
(function ($) {
	jQuery.fn.bitly = function(params) {
		var params = params || {};
	
		var $this = $(this);
		var fromUrl = false;
		var longUrl = params.longUrl || '';
	
		if ($this.attr('href')) {
			longUrl = $this.attr('href'); // We can grab this from a jQuery element.
			fromUrl = true;
		}
	
		$.ajax({
			url: '/ajax/externalAPI/bitlyEncode',
			data: {longUrl : longUrl},
			dataType: 'json',
			success: function (data) {
				
				// Make the library fail gracefully back to the
				// original long-form URL.
				var url = '';
				if (data.url > '') {
					url = data.url;
				} else {
					url = longUrl;
				}
	
				if (fromUrl && url) { // If we provided an element, we assume it's a link and set the HREF.
					$this.attr('href', url);
				}
			
				if (params && params.callback) { // You can also set a callback.
				   params.callback(url);
				}
			}
		});
	};
})(jQuery);

/**
 * Given a set of DOM elements for, timezoneID, currencyCode, country, state, and city 
 * calls the AJAX controller for geo-location information and attempts to populate the fields.
 */
(function ($) {
	$.geolocation = function(params) {
	
		var params = params || {};
		var ip = params.ip || '208.124.246.46';
		var $timezoneID = params.timezoneID || false;
		var $currencyCode = params.currencyCode || false;
		var $country = params.country || false;
		var $state = params.state || false;
		var $city = params.city || false;
		
		// Store the initial values in-case there is a lag and the user has
		// already set a value.
		var initialValues = {};
		var setInitialValue = function(key, $selector) {
			if ($selector) {
				initialValues[key] = $selector.val();
			} else {
				initialValues[key] = false;
			}
		}
		setInitialValue('timezoneID', $timezoneID);
		setInitialValue('currencyCode', $currencyCode);
		setInitialValue('country', $country);
		setInitialValue('state', $state);
		setInitialValue('city', $city);
		
		// Check whether a selector has the same value as its initial value.
		var isInitialValue = function(key, $selector) {
			var currentValue = $selector.val();
			var initialValue = initialValues[key];
			return initialValue == currentValue;
		}
				
		// Use our locale information lookup to populate fields.
		$.ajax({
			url: '/ajax/setup/getLocaleInformation',
			data: {ip : ip},
			dataType: 'json',
			success: function (data) {
			
	
				var timezoneID = data['timezoneID'];
				if (timezoneID > '' && $timezoneID && isInitialValue('timezoneID', $timezoneID)) { // If timezoneID is a non-empty string.
					$timezoneID.val(timezoneID).trigger('change'); // Trigger elements in case of dependent events.
				}

				var currencyCode = data['currencyCode'];
				if (currencyCode > '' && $currencyCode && isInitialValue('currencyCode', $currencyCode)) { // If Currency Code is a non-empty string.
					$currencyCode.val(currencyCode).trigger('change');
				}
		
				// Populate the country/state/city fields due to triggers on country
				// We must populate city and province asynchronously.
				if (data.country > '') {
				
					// There is a good chance the province/state drop-down is dependent
					// on an event triggered by the country drop-down. For this reason
					// a callback is used to order operations.
					var countryChangeCallback = function() {
						if (data.state > '' && $state && isInitialValue('state', $state)) {
							$state.filter(':visible').val(data.state);
						}
						if (data.city > '' && $city && isInitialValue('city', $city)) {
							$city.val(data.city);
						}
					}
			
					if ($country && isInitialValue('country', $country)) {
						$country.val(data.country);
						$country.trigger('change');
						countryChangeCallback();
					} else {
						countryChangeCallback();
					}
				}
				if (params && params.success) {
					params.success.apply(this);
				}
			}
		});
	};
})(jQuery);