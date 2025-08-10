mergeInto(LibraryManager.library, {
    LoadURL: function (urlPtr) {
        var url = UTF8ToString(urlPtr);
        window.location.href = url;
    }
});
