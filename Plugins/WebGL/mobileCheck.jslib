mergeInto(LibraryManager.library, {
    IsMobile: function () {
        if (/android|iphone|ipad|mobile/i.test(navigator.userAgent)) {
            return 1;
        }
        return 0;
    }
});
