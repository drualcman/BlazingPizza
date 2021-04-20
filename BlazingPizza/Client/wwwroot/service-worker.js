//install service worker
self.addEventListener('install', async event => {
    console.log('Install Service Worker...');
    self.skipWaiting();
});

self.addEventListener('fetch', event => {

    //implements can work off line
    //here must be the code to get the data from the browser cache
    return null;            //cheat the browser and avoid off line content and request to the server for the content
});

self.addEventListener('push', event => {
    const payload = event.data.json();
    event.waitUntil(
        self.registration.showNotification('Blazing Pizza', {
            body: payload.message,
            icon: 'images/icon-512.png',
            ibrate: [100, 50, 100],
            data: { url: payload.url }
        })
    );
});

self.addEventListener('notificationclick', event => {
    event.notification.close();
    event.waitUntil(clients.openWindow(event.notification.data.url));
});