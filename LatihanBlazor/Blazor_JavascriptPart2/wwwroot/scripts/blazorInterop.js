var blazorInterop = blazorInterop || {};

blazorInterop.showAlert = function (message) {
    alert(message);
};

blazorInterop.focusElement = function (element) {
    element.focus();
};


blazorInterop.focusElementById = function (id) {
    var element = document.getElementById(id);
    if (element) element.focus();
};

blazorInterop.throwsError = function () {
    throw Error("Error from JS Function");
}