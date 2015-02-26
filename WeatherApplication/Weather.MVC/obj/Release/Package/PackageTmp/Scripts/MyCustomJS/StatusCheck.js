var onlineStatus = navigator.onLine;
if (onlineStatus == true) {
    alert('ONLINE!');
}
else {
    alert('OFFLINE!');
}

window.applicationCache.onchecking = function (e) {
    updateCacheStatus('Checking for a new version of the application.');
};
window.applicationCache.ondownloading = function (e) {
    updateCacheStatus('Downloading a new offline version of the application');
};

window.applicationCache.oncached = function (e) {
    updateCacheStatus('The application is available offline.');
};

window.applicationCache.onerror = function (e) {
    updateCacheStatus('Something went wrong while updating the offline version of the application. It will not be available offline.');
};

window.applicationCache.onupdateready = function (e) {
    window.applicationCache.swapCache();
    updateCacheStatus('The application was updated. Refresh for the changes to take place.');
};

window.applicationCache.onnoupdate = function (e) {
    updateCacheStatus('The application is also available offline.');
};

window.applicationCache.onobsolete = function (e) {
    updateCacheStatus('The application cannot be updated, no manifest file was found.');
};

window.applicationCache.onprogress = function (e) {
    var message = 'Downloading offline resources.. ';
    if (e.lengthComputable) {
        updateCacheStatus(message + Math.round(e.loaded / e.total * 100) + '%');
    } else {
        updateCacheStatus(message);
    };
};