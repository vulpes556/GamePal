"use client"

import { useState } from "react"
import PasswordField from "@/components/PasswordField/PasswordField"
import { registerUser } from "@/scripts/scripts"
import { useRouter } from "next/navigation"

export default function Signup() {
    const router = useRouter();
    const [errors, setErrors] = useState({
        emailErrors: [],
        passwordErrors: [],
        usernameErrors: [],
    })

    const handleSignup = async (e) => {
        e.preventDefault()
        const formData = new FormData(e.target)
        const check = checkFormData(formData)

        if (check.valid) {
            const email = formData.get("email")
            const password = formData.get("password")
            const username = formData.get("username")

            await registerUser({ email, password, username })
            router.push("/login");

        } else {
            setErrors(check.errors)
        }
    }


    function checkFormData(formData) {
        const email = formData.get("email")?.trim()
        const username = formData.get("username")?.trim()
        const password = formData.get("password")
        const confirmPassword = formData.get("password2")

        const errors = {
            emailErrors: [],
            passwordErrors: [],
            usernameErrors: [],
        }

        if (!username || username.length < 3) {
            errors.usernameErrors.push("Username must be at least 3 characters.")
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        if (!email || !emailRegex.test(email)) {
            errors.emailErrors.push("Please enter a valid email address.")
        }

        if (!password || password.length < 6) {
            errors.passwordErrors.push("Password must be at least 6 characters.")
        }

        if (!/[A-Z]/.test(password)) {
            errors.passwordErrors.push("Password must contain at least one uppercase letter.")
        }

        if (!/[a-z]/.test(password)) {
            errors.passwordErrors.push("Password must contain at least one lowercase letter.")
        }

        if (!/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
            errors.passwordErrors.push("Password must contain at least one special character.")
        }

        if (password !== confirmPassword) {
            errors.passwordErrors.push("Passwords do not match.")
        }

        const hasErrors =
            errors.emailErrors.length > 0 ||
            errors.passwordErrors.length > 0 ||
            errors.usernameErrors.length > 0

        if (hasErrors) {
            return { valid: false, errors }
        }

        return { valid: true }
    }




    return (
        <div className="login-page-main">
            <div className="login-box">
                <div className="login-form">
                    <form onSubmit={handleSignup}>
                        <label htmlFor="username">Username</label>
                        <input type="text" name="username" id="username" />
                        {errors.usernameErrors.map((err, i) => (
                            <div key={i} className="error">{err}</div>
                        ))}

                        <label htmlFor="email">Email</label>
                        <input type="email" name="email" id="email" />
                        {errors.emailErrors.map((err, i) => (
                            <div key={i} className="error">{err}</div>
                        ))}

                        <label htmlFor="password">Password</label>
                        <PasswordField id="password" name="password" />

                        <label htmlFor="confirm">Confirm Password</label>
                        <PasswordField id="password2" name="password2" />
                        {errors.passwordErrors.map((err, i) => (
                            <div key={i} className="error">{err}</div>
                        ))}

                        <button type="submit" className="primary-button">Sign Up</button>
                    </form>
                </div>
            </div>
        </div>
    )
}