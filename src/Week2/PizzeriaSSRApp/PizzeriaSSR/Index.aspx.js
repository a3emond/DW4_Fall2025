document.addEventListener("DOMContentLoaded", function () {
    // Controls
    const form = document.getElementById(clientIDs.form);
    const name = document.getElementById(clientIDs.name);
    const phone = document.getElementById(clientIDs.phone);
    const address = document.getElementById(clientIDs.address);
    const delivery = document.getElementById(clientIDs.delivery);
    const quantity = document.getElementById(clientIDs.quantity);

    // Error labels
    const nameErr = document.getElementById(clientIDs.nameErr);
    const phoneErr = document.getElementById(clientIDs.phoneErr);
    const addrErr = document.getElementById(clientIDs.addrErr);
    const qtyErr = document.getElementById(clientIDs.qtyErr);

    // Helper: reset all error messages
    function resetErrors() {
        [nameErr, phoneErr, addrErr, qtyErr].forEach(l => {
            l.style.display = "none";
            l.innerText = "";
        });
    }

    // Helper: show an error
    function showError(label, message) {
        label.innerText = message;
        label.style.display = "inline";
    }

    // Validation functions
    function validateName() {
        if (name.value.trim().length < 2) {
            showError(nameErr, "Veuillez entrer un nom valide.");
            return false;
        }
        return true;
    }

    function validatePhone() {
        let digits = phone.value.replace(/\D/g, ""); // keep only numbers
        if (digits.length === 10) {
            phone.value = `(${digits.substring(0, 3)}) ${digits.substring(3, 6)}-${digits.substring(6)}`;
            return true;
        }
        showError(phoneErr, "Veuillez entrer un numero de telephone valide (10 chiffres).");
        return false;
    }

    function validateAddress() {
        if (delivery.checked && address.value.trim().length < 5) {
            showError(addrErr, "Veuillez entrer une adresse pour la livraison.");
            return false;
        }
        return true;
    }

    function validateQuantity() {
        let qty = parseInt(quantity.value, 10);
        if (isNaN(qty) || qty < 1) {
            showError(qtyErr, "Veuillez entrer une quantite valide (min. 1).");
            return false;
        }
        quantity.value = qty; // normalize
        return true;
    }

    // Attach validation on submit
    form.onsubmit = function (e) {
        resetErrors();

        const valid =
            validateName() &
            validatePhone() &
            validateAddress() &
            validateQuantity();

        if (!valid) {
            e.preventDefault();
            return false;
        }
        return true;
    };
});
