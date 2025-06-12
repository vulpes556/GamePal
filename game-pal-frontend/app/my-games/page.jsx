import {auth} from "@/auth.js"

export default async function MyGames(){
    const session = await auth()
    return (
        <div className="my-games-main">
            {console.log(session)}
            my games

            Currently, you dont have any games!
            Try to add some!
            <button className="primary-button">+</button>
        </div>
    )
}