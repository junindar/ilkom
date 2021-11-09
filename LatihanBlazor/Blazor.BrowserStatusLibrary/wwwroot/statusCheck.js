
window.browserCheckLib = {
    registerOnlineHandler: function (dotNetObjectRef) {
        function onlineHandler() {
            dotNetObjectRef.invokeMethodAsync("SetOnlineStatus",
                navigator.onLine);
        };

        // Set up initial values
        onlineHandler();

        // Register event handler
        window.addEventListener("online", onlineHandler);
        window.addEventListener("offline", onlineHandler);
    }
};