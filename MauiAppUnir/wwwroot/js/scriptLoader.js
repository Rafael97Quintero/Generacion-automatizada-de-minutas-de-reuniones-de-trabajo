window.loadScript = (url, id) => {
    return new Promise((resolve, reject) => {
        if (document.getElementById(id)) {
            resolve();
            return;
        }

        const script = document.createElement('script');
        script.src = url;
        script.id = id;
        script.onload = resolve;
        script.onerror = reject;
        document.head.appendChild(script);
    });
};