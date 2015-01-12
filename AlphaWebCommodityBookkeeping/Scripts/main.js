requirejs.config({
    shim: {
        'jqueryui': ['jquery'],
        'jqueryvalidate': ['jquery'],
        'jqModal': ['jquery']
    }
    //baseUrl: '../Scripts/helper'
});



require(["../Scripts/helper/jquery", "../Scripts/helper/jqueryui", "../Scripts/helper/jqueryvalidate", "../Scripts/helper/jqModal"], function ($, jquery, jqueryui, jqueryvalidate, jqModal) {
    //app.Start();

    
    //This function is called when scripts/helper/util.js is loaded.
    //If util.js calls define(), then this function is not fired until
    //util's dependencies have loaded, and the util argument will hold
    //the module value for "helper/util".
});

