import Link from "next/link"
export default function Login() {
    return (
        <div className="login-page-main">
            <div className="login-box">
                <form action="">
                    <label htmlFor="username/email">Username/Email</label>
                    <input type="text" name="username/email" id="username/email" />
                    <label htmlFor="password">Password</label>
                    <input type="text" name="password" id="password" />
                    <button className="primary-button">Log In</button>
                </form>
                <div className="">
                    <span>Don't have an account yet?</span>
                    <Link href={"/create"}>Create one</Link>
                </div>
            </div>
        </div>
    )
}