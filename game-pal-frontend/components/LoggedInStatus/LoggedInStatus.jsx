import { auth } from "@/auth.js"

export default async function LoggedInStatus() {
    const session = await auth()

    if (!session?.user) return (
        <>
            Logged out
        </>
    )

    return (
        <div>
            logged in, username: {session.user.name} , userId: {session.user.id}, userEmail: {session.user.email}
        </div>
    )
}