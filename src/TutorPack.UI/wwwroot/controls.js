// Blazor handles selection and focus. Suppress competing browser actions first.
document.addEventListener("keydown", event => {
    if (!(event.target instanceof Element)) return;
    const navigatesTabs = event.target.closest('.tp-tabs [role="tab"]')
        && ["ArrowLeft", "ArrowRight", "Home", "End"].includes(event.key);
    // Escape closes an open select before it can dismiss the containing dialog.
    const closesSelect = event.key === "Escape" && event.target.closest('.material-select.open');
    if (navigatesTabs || closesSelect) {
        event.preventDefault();
    }
}, { capture: true });

window.tutorPackControls = {
    showDialog(dialog) {
        if (!dialog || dialog.open) return;
        const opener = document.activeElement;
        // Blazor may remove the component directly instead of calling dialog.close().
        const removal = new MutationObserver(() => {
            if (dialog.isConnected) return;
            removal.disconnect();
            if (document.activeElement === document.body && opener?.isConnected) opener.focus();
        });
        removal.observe(document.body, { childList: true, subtree: true });
        dialog.addEventListener("click", event => {
            if (event.target !== dialog) return;
            const bounds = dialog.getBoundingClientRect();
            if (event.clientX < bounds.left || event.clientX > bounds.right
                || event.clientY < bounds.top || event.clientY > bounds.bottom) {
                dialog.close();
            }
        });
        dialog.showModal();
    }
};
