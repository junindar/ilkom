var blazorInterop = blazorInterop || {};

blazorInterop.showAlert=function(message) {
    alert(message);
};



blazorInterop.showPrompt = function (message,defaultValue) {
    return prompt(message, defaultValue);
};

blazorInterop.createBook = function (judul, penulis) {
    let book = 
        {
            judul: judul,
            penulis: penulis,
            penerbit: "Cahaya Buku"
            
        };
    return book;
};



