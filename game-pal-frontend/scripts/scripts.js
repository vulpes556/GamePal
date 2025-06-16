const backendUrl = process.env.BACKEND_URL;

export async function fetchUserGames() {
    const response = await fetch("/api/user-games", {
        method: "GET",
        headers: {},
    });

    if (!response.ok) {
        throw new Error("Something went wrong");
    }
    const result = await response.json();
    return result;
};


export async function registerUser(registrationData) {
    const response = await fetch("/api/user/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(registrationData)
    });

    if (!response.ok) {
        const errorData = await response.json();
        console.error("Registration failed:", errorData);
        throw new Error("Error during registration", errorData)
    }
    const data = await response.json();
    return data;
}


export async function fetchGames(session) {
    const res = await fetch(`${backendUrl}/games`, {
        headers: {
            Authorization: `Bearer ${session.accessToken}`,
            "Content-Type": "application/json",
        },

    });
    if (!res.ok) {
        throw new Error(`Failed to fetch games: ${res.status}`);
    }
    return await res.json();
}

export async function addGameToUserLibrary(addGameToUserRequest) {
    const res = await fetch(`${backendUrl}/api/user/add-game`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(addGameToUserRequest)
    })
    if (!res.ok) {
        throw new Error(`Failed to add game to user's library: ${res.status}`);
    }
    return await res.json();
}