// ======================================================
// API Helper with normalization
// ======================================================
function normalizeResponse(obj) {
    if (!obj || typeof obj !== "object") return obj;
    const normalized = {};
    for (const key in obj) {
        if (Object.prototype.hasOwnProperty.call(obj, key)) {
            normalized[key.toLowerCase()] = obj[key];
        }
    }
    return normalized;
}

async function apiPost(url, data) {
    const res = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/x-www-form-urlencoded" },
        body: new URLSearchParams(data)
    });
    return normalizeResponse(await res.json());
}

// ======================================================
// Shared Validation Utilities
// ======================================================
function showValidation(feedbackElement, messages) {
    feedbackElement.innerHTML = "";
    messages.forEach(m => {
        const li = document.createElement("li");
        li.textContent = m.message;
        if (m.valid) li.classList.add("valid");
        feedbackElement.appendChild(li);
    });
}

function validateEmail(value) {
    return /\S+@\S+\.\S+/.test(value);
}

const passwordChecks = [
    { regex: /.{8,}/, message: "At least 8 characters" },
    { regex: /[A-Z]/, message: "At least one uppercase letter" },
    { regex: /[a-z]/, message: "At least one lowercase letter" },
    { regex: /\d/, message: "At least one number" },
    { regex: /[^A-Za-z0-9]/, message: "At least one special character" }
];

// ======================================================
// Login Form
// ======================================================
const loginForm = document.getElementById("login-form");
if (loginForm) {
    const loginEmail = document.getElementById("email");
    const loginPassword = document.getElementById("password");
    const emailFeedback = document.getElementById("email-feedback");
    const passwordFeedback = document.getElementById("password-feedback");
    const formError = document.getElementById("form-error");

    loginEmail.addEventListener("input", () => {
        showValidation(emailFeedback, [
            { message: "Must be a valid email format", valid: validateEmail(loginEmail.value) }
        ]);
    });

    loginPassword.addEventListener("input", () => {
        const messages = passwordChecks.map(check => ({
            message: check.message,
            valid: check.regex.test(loginPassword.value)
        }));
        showValidation(passwordFeedback, messages);
    });

    loginForm.addEventListener("submit", async e => {
        e.preventDefault();
        formError.textContent = "";

        if (!validateEmail(loginEmail.value)) {
            formError.textContent = "Please enter a valid email.";
            return;
        }

        const passValid = passwordChecks.every(check => check.regex.test(loginPassword.value));
        if (!passValid) {
            formError.textContent = "Password does not meet requirements.";
            return;
        }

        const result = await apiPost("/api/login", {
            email: loginEmail.value,
            password: loginPassword.value
        });

        if (result.success) {
            location.href = "/";
        } else {
            formError.textContent = result.error || result.message || "Login failed.";
        }
    });
}

// ======================================================
// Register Form
// ======================================================
const registerForm = document.getElementById("register-form");
if (registerForm) {
    const username = document.getElementById("username");
    const email = document.getElementById("email");
    const password = document.getElementById("password");
    const confirm = document.getElementById("confirm");

    const emailFeedback = document.getElementById("email-feedback");
    const passwordFeedback = document.getElementById("password-feedback");
    const confirmFeedback = document.getElementById("confirm-feedback");
    const formError = document.getElementById("form-error");

    email.addEventListener("input", () => {
        showValidation(emailFeedback, [
            { message: "Must be a valid email format", valid: validateEmail(email.value) }
        ]);
    });

    password.addEventListener("input", () => {
        const messages = passwordChecks.map(check => ({
            message: check.message,
            valid: check.regex.test(password.value)
        }));
        showValidation(passwordFeedback, messages);
        confirm.dispatchEvent(new Event("input"));
    });

    confirm.addEventListener("input", () => {
        const match = confirm.value === password.value && password.value.length > 0;
        showValidation(confirmFeedback, [
            { message: match ? "Passwords match" : "Passwords do not match", valid: match }
        ]);
    });

    registerForm.addEventListener("submit", async e => {
        e.preventDefault();
        formError.textContent = "";

        const passValid = passwordChecks.every(check => check.regex.test(password.value));
        const emailValid = validateEmail(email.value);
        const confirmValid = confirm.value === password.value;

        if (!emailValid || !passValid || !confirmValid) {
            formError.textContent = "Please fix validation errors before submitting.";
            return;
        }

        const result = await apiPost("/api/register", {
            username: username.value,
            email: email.value,
            password: password.value
        });

        if (result.success) {
            location.href = "/login";
        } else {
            formError.textContent = result.error || result.message || "Registration failed.";
        }
    });
}
