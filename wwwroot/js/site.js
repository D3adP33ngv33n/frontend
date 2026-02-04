// Scroll listener for navbar
window.setupScrollListener = function (dotNetHelper) {
    window.addEventListener('scroll', function () {
        dotNetHelper.invokeMethodAsync('OnScroll', window.scrollY);
    });
};
