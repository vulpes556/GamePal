import PasswordField from "@/components/PasswordField/PasswordField"
import { redirect } from "next/navigation"
import { AuthError } from "next-auth"


const SIGNIN_ERROR_URL = "/error"

async function handleSignup(params) {
    "use server"
}

export default async function Signup(props) {
    return (
        <div className="login-page-main">
            <div className="login-box">
                <div className="login-form">
                    <form action={handleSignup}>
                        <label htmlFor="username">Username</label>
                        <input type="text" name="username" id="username" />
                        <label htmlFor="email"> Email</label>
                        <input type="email" name="email" id="email" />
                        <label htmlFor="password"> Password </label>
                        <PasswordField id="password" name="password" />
                        <label htmlFor="confirm">Confirm password</label>
                        <PasswordField id="password2" name="password2"></PasswordField>
                        <button type="submit" className="primary-button">Sign Up</button>
                    </form>
                </div>
            </div>
        </div>
    )
}