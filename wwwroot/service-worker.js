// In development, always fetch from network (no caching).
// This is replaced by service-worker.published.js during dotnet publish.
self.addEventListener('fetch', () => { });
