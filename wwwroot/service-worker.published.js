// Blazor PWA service worker (published builds only).
// Uses the auto-generated asset manifest for content-hash-based caching.
importScripts('./service-worker-assets.js');

const CACHE_PREFIX = 'nostdlib-offline-cache-';
const CACHE_NAME = `${CACHE_PREFIX}${self.assetsManifest.version}`;
const OFFLINE_URL = '/';

self.addEventListener('install', event => {
    event.waitUntil(
        (async () => {
            const cache = await caches.open(CACHE_NAME);
            // Cache every asset listed in the manifest (each URL includes its content hash)
            const assets = self.assetsManifest.assets
                .filter(a => a.url !== 'service-worker-assets.js')
                .map(a => new Request(a.url, { integrity: a.hash, cache: 'no-cache' }));
            await Promise.all(assets.map(req => cache.add(req).catch(() => { })));
            await self.skipWaiting();
        })()
    );
});

self.addEventListener('activate', event => {
    event.waitUntil(
        (async () => {
            // Delete every old cache that starts with our prefix
            const keys = await caches.keys();
            await Promise.all(
                keys.filter(k => k.startsWith(CACHE_PREFIX) && k !== CACHE_NAME)
                    .map(k => caches.delete(k))
            );
            await self.clients.claim();
        })()
    );
});

self.addEventListener('fetch', event => {
    if (event.request.method !== 'GET') return;
    if (!event.request.url.startsWith(self.location.origin)) return;

    // Navigation (index.html) — always go to network so the app bootstraps fresh
    if (event.request.mode === 'navigate') {
        event.respondWith(
            fetch(event.request, { cache: 'no-cache' })
                .catch(() => caches.match(OFFLINE_URL))
        );
        return;
    }

    // All other assets — cache first (matched by content hash), network fallback
    event.respondWith(
        caches.match(event.request).then(cached => cached || fetch(event.request))
    );
});

self.addEventListener('message', event => {
    if (event.data?.type === 'SKIP_WAITING') {
        self.skipWaiting();
    }
});
