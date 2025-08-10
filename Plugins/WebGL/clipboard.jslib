mergeInto(LibraryManager.library, {
    CopyToClipboard: function(textPtr) {
        var text = UTF8ToString(textPtr);
        navigator.clipboard.writeText(text).then(function() {
            console.log("Texto copiado!");
        }).catch(function(err) {
            console.error("Erro ao copiar: ", err);
        });
    }
});