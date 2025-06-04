import PasswordField from "@/components/PasswordField/PasswordField"
import Link from "next/link"
import { redirect } from "next/navigation"
import { signIn, auth, providerMap } from "../../auth.js"
import { AuthError } from "next-auth"


const SIGNIN_ERROR_URL = "/error"

export default async function Login(props) {
  return (
    <div className="login-page-main">
      <div className="login-box">
        <div className="login-form">
          <form
            action={async (formData) => {
              "use server"
              const email = formData.get("email")
              const password = formData.get("password")
              try {
                const result = await signIn("credentials", {
                  redirect: true,
                  email,
                  password,
                  callbackUrl: "/",
                  redirectTo: "/"
                })
              } catch (error) {
                if (error instanceof AuthError) {
                  return redirect(`${SIGNIN_ERROR_URL}?error=${error.type}`)
                }
                throw error
              }
            }}
          >
            <label htmlFor="email">
              Username/Email
            </label>
            <input name="email" id="email" />
            <label htmlFor="password">
              Password
            </label>
            <PasswordField id="password" name="password" />
            <button type="submit" className="primary-button">Log In</button>
          </form>
        </div>
        {Object.values(providerMap).map((provider) => (
          <form
            key={provider.id}
            action={async () => {
              "use server"
              try {
                await signIn(provider.id, {
                  redirectTo: props.searchParams?.callbackUrl ?? "",
                })
              } catch (error) {
                if (error instanceof AuthError) {
                  return redirect(`${SIGNIN_ERROR_URL}?error=${error.type}`)
                }
                throw error
              }
            }}
          >
            <button type="submit" className="primary-button">
              <span>Sign in with {provider.name}</span>
            </button>
          </form>
        ))}
      </div>

    </div>
  )
}