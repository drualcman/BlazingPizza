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