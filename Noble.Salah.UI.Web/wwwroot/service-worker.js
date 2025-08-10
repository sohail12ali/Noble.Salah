// Service worker for Noble Salah application
// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

self.addEventListener('install', event => {
    console.log('Service Worker installing...');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('Service Worker activating...');
    event.waitUntil(self.clients.claim());
});

self.addEventListener('fetch', event => {
    // For development, always fetch from network
    // For production, you might want to add caching strategies here
    event.respondWith(fetch(event.request).catch(error => {
        console.log('Fetch failed:', error);
        // Return a fallback response for navigation requests
        if (event.request.mode === 'navigate') {
            return new Response('Network error occurred. Please check your connection and try again.', {
                status: 503,
                statusText: 'Service Unavailable',
                headers: { 'Content-Type': 'text/html' }
            });
        }
        throw error;
    }));
});

// Handle navigation errors gracefully
self.addEventListener('error', event => {
    console.log('Service Worker error:', event.error);
});

self.addEventListener('unhandledrejection', event => {
    console.log('Service Worker unhandled rejection:', event.reason);
});