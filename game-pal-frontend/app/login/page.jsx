import PasswordField from "@/components/PasswordField/PasswordField"
import Link from "next/link"
export default function Login() {
  return (
    <div className="login-page-main">
      <div className="login-box">
        <div className="login-form">
          <form action="">
            <label htmlFor="username/email">Username/Email</label>
            <input type="text" name="username/email" id="username/email" />
            <label htmlFor="password">Password</label>
            <PasswordField id="password" name="password" />
            <button className="primary-button">Log In</button>
          </form>
        </div>
        <div className="account-creation">
          <span>Don't have an account yet?</span>
          <Link href={"/create"}>Create one</Link>
        </div>

      </div>
    </div>
  )
}