// Scroll listener for navbar
window.setupScrollListener = function (dotNetHelper) {
    window.addEventListener('scroll', function () {
        dotNetHelper.invokeMethodAsync('OnScroll', window.scrollY);
    });
};

// File download helper for Blazor
window.downloadFile = function (fileName, base64Data) {
    const byteChars = atob(base64Data);
    const byteNumbers = new Array(byteChars.length);
    for (let i = 0; i < byteChars.length; i++) {
        byteNumbers[i] = byteChars.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};

// Touch slider functionality
window.sliderTouch = {
    instances: new Map(),

    init: function (elementId, dotNetHelper) {
        const element = document.getElementById(elementId);
        if (!element) return;

        const state = {
            startX: 0,
            startY: 0,
            currentX: 0,
            isDragging: false,
            isHorizontalSwipe: null,
            threshold: 50,
            dotNetHelper: dotNetHelper
        };

        element.addEventListener('touchstart', (e) => {
            state.startX = e.touches[0].clientX;
            state.startY = e.touches[0].clientY;
            state.currentX = state.startX;
            state.isDragging = true;
            state.isHorizontalSwipe = null;
        }, { passive: true });

        element.addEventListener('touchmove', (e) => {
            if (!state.isDragging) return;

            const deltaX = e.touches[0].clientX - state.startX;
            const deltaY = e.touches[0].clientY - state.startY;

            // Determine swipe direction on first significant movement
            if (state.isHorizontalSwipe === null && (Math.abs(deltaX) > 10 || Math.abs(deltaY) > 10)) {
                state.isHorizontalSwipe = Math.abs(deltaX) > Math.abs(deltaY);
            }

            // Only handle horizontal swipes
            if (state.isHorizontalSwipe) {
                e.preventDefault();
                state.currentX = e.touches[0].clientX;
            }
        }, { passive: false });

        element.addEventListener('touchend', () => {
            if (!state.isDragging) return;

            const deltaX = state.currentX - state.startX;

            if (state.isHorizontalSwipe && Math.abs(deltaX) > state.threshold) {
                if (deltaX > 0) {
                    state.dotNetHelper.invokeMethodAsync('OnSwipePrev');
                } else {
                    state.dotNetHelper.invokeMethodAsync('OnSwipeNext');
                }
            }

            state.isDragging = false;
            state.isHorizontalSwipe = null;
        }, { passive: true });

        this.instances.set(elementId, state);
    },

    dispose: function (elementId) {
        this.instances.delete(elementId);
    }
};
