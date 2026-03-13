document.addEventListener("DOMContentLoaded", () => {

    const antiForgeryTokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!antiForgeryTokenInput) return;

    const antiForgeryToken = antiForgeryTokenInput.value;

    document.querySelectorAll(".add-to-cart-btn").forEach(button => {

        button.addEventListener("click", async function () {

            const productId = this.dataset.productId;
            const productName = this.dataset.productName;

            try {

                const response = await fetch("/Cart/Add", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
                        "RequestVerificationToken": antiForgeryToken
                    },
                    body: `productId=${encodeURIComponent(productId)}`
                });

                if (!response.ok) {
                    throw new Error();
                }

                const data = await response.json();

                if (data.success) {

                    const badge = document.querySelector(".cart-badge");

                    if (badge) {
                        badge.textContent = data.cartCount;
                        badge.classList.remove("d-none");
                    }

                    showCartToast(`${productName} беше добавен в количката.`);
                }

            } catch {

                showCartToast("Грешка при добавяне в количката.", true);

            }

        });

    });

});


function showCartToast(message, isError = false) {

    const toast = document.getElementById("cart-toast");
    const toastMessage = document.getElementById("cart-toast-message");

    if (!toast || !toastMessage) return;

    toastMessage.textContent = message;

    toast.classList.remove("show", "error");

    if (isError) {
        toast.classList.add("error");
    }

    requestAnimationFrame(() => {
        toast.classList.add("show");
    });

    clearTimeout(toast.hideTimeout);

    toast.hideTimeout = setTimeout(() => {
        toast.classList.remove("show");
    }, 2500);
}