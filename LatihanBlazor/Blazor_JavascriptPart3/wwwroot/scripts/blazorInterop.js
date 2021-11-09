var blazorInterop = blazorInterop || {};


blazorInterop.methodCalculate = function () {
    var promise = DotNet.invokeMethodAsync("Blazor_JavascriptPart3",
        "Calculate", 5);
    promise.then(result => alert(result));
};

blazorInterop.methodCalculateCustomIdentifier = function () {
    var promise = DotNet.invokeMethodAsync("Blazor_JavascriptPart3",
        "CalculateWithVal2", 5, 6);
    promise.then(result => alert(result));
};


blazorInterop.sayHello1 = (dotNetHelper) => {
    return dotNetHelper.invokeMethodAsync('GetHelloMessage');
};

blazorInterop.callDotNetSetWindowSizeMethod = function (dotNetObjectRef) {
    dotNetObjectRef.invokeMethodAsync("SetWindowSize",
        {
            width: window.innerWidth,
            height: window.innerHeight
        });
};


blazorInterop.registerResizeHandler = function (dotNetObjectRef) {
    function resizeHandler() {
        dotNetObjectRef.invokeMethodAsync("SetWindowSize",
            {
                width: window.innerWidth,
                height: window.innerHeight
            });
    };

    // Set up initial values
    resizeHandler();

    // Register event handler
    window.addEventListener("resize", resizeHandler);
};





blazorInterop.startRandomGenerator = function (dotNetObject) {
    setInterval(function () {
        let text = Math.random() * 1000;
        console.log("JS: Generated " + text);
        dotNetObject.invokeMethodAsync('AddText', text.toString());
    }, 1000);
};

