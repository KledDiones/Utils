// Merge our module into framework.js
mergeInto(LibraryManager.library, {

    transparentBackground: function(mask) {
    // If mask of the layer is set to "depth only"
        if (mask == 0x00004000)
        {
            var v = GLctx.getParameter(GLctx.COLOR_WRITEMASK);
            // And the color is "transparent"
            if (!v[0] && !v[1] && !v[2] && v[3])
                // We do nothing
                return;
        }
        // else we clear the mask to prevent tearing
        GLctx.clear(mask);
    },

     CallToastInfo: function (textMsg) {
        window.parent.postMessage("ToastMessageInfo$$"+UTF8ToString(textMsg), "*");
     },

     CallToastSuccess: function (textMsg) {
        window.parent.postMessage("ToastMessageSuccess$$"+UTF8ToString(textMsg), "*");
     },

     CallToastError: function (textMsg) {
        window.parent.postMessage("ToastMessageError$$"+UTF8ToString(textMsg), "*");
     },

     UpdateUserWallet: function () {
     //Message from IFRAME TO PARENT PAGE DIRECTLY
        window.parent.postMessage("UpdateUserWallet", "*");
     },

     DismissLoadingScreen: function () {
        window.parent.postMessage("RemoveLoadingScreen", "*");
    },

    RedirectToDeposit: function () {
        window.parent.postMessage("RedirectToDeposit", "*");
    },

    AnalyticsEvents: function (eventType, eventParameters) {
    console.log("Put here analytics when done");
        window.parent.postMessage("AnalyticsEvents$$"+UTF8ToString(eventType)+"??"+UTF8ToString(eventParameters), "*");
    },

     IsMobile: function () {
        var ua = window.navigator.userAgent.toLowerCase();
        var mobilePattern = /android|iphone|ipad|ipod/i;

        return ua.search(mobilePattern) !== -1 || (ua.indexOf("macintosh") !== -1 && "ontouchend" in document);
    },

});