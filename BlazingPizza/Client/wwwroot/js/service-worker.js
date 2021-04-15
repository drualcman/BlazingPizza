self.addEventListener('install', async event => {
    console.log('Install Service Worker...');
    self.skipWaiting();
});