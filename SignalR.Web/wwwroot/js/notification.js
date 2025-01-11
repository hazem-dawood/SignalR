const notificationService = {
    requestPermission() {
        if ("Notification" in window) {
            Notification.requestPermission().then(permission => {
                console.log(`Notification permission: ${permission}`);
            });
        } else {
            console.error("Browser does not support notifications.");
        }
    },

    showNotification(title, options) {
        if (Notification.permission === "granted") {
            const notification = new Notification(title, options);
            notification.onclick = () => {
                console.log("Notification clicked.");
                if (options.onClickUrl) {
                    window.open(options.onClickUrl, "_blank");
                }
            };
            notification.onclose = () => {
                console.log("Notification closed.");
            };
        } else {
            console.error("Notifications are not allowed.");
        }
    },

    notify(title, message, icon, onClickUrl = null) {
        this.showNotification(title, {
            body: message,
            icon: icon,
            onClickUrl: onClickUrl
        });
    }
};