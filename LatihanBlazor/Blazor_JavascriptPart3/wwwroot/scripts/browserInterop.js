var jsObjectRefs = {};
var jsObjectRefId = 0;
const jsRefKey = '__jsObjectRefId';
DotNet.attachReviver(function (key, value) {
    if (value &&
        typeof value === 'object' &&
        value.hasOwnProperty(jsRefKey) &&
        typeof value[jsRefKey] === 'number') {

        var id = value[jsRefKey];
        if (!(id in jsObjectRefs)) {
            throw new Error("This JS object reference does not exists : " + id);
        }
        const instance = jsObjectRefs[id];
        return instance;
    } else {
        return value;
    }
});
function cleanObjectRef(id) {
    console.log(jsObjectRefId);
    delete jsObjectRefs[jsObjectRefId];
}
function storeObjectRef(obj) {
    var id = jsObjectRefId++;
    jsObjectRefs[id] = obj;
    var jsRef = {};
    jsRef[jsRefKey] = id;
    return jsRef;
}
function openWindow() {
    return storeObjectRef(window.open("/", "_blank"));
}
function closeWindow(window) {
    window.close();
}
