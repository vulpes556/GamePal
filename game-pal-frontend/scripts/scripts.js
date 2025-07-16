const backendUrl = process.env.BACKEND_URL;

export async function fetchUserGames() {
    const response = await fetch("/backend/user-games", {
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
    const response = await fetch("/backend/user/register", {
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

export async function fetchGames(page = 1, pageSize = 10, genre, name, platform) {
    const url = new URL(`${backendUrl}/games`);
    url.searchParams.set("page", page);
    url.searchParams.set("pageSize", pageSize);

    if (genre) url.searchParams.set("genre", genre);
    if (name) url.searchParams.set("name", name);
    if (platform) url.searchParams.set("platform", platform);


    const res = await fetch(url.toString(), {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!res.ok) {
        throw new Error(`Failed to fetch games: ${res.status}`);
    }

    return await res.json();
}


export async function addGameToUserLibrary(addGameToUserRequest) {
    const res = await fetch("/api/user/add-game", {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(addGameToUserRequest),
    });
    if (!res.ok) throw new Error(`Add game failed: ${res.status}`);
    return res.json();
}


export async function getGameCategories() {
    const res = await fetch(`backend/categories`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!res.ok) {
        throw new Error(`Failed to fetch categories: ${res.status}`);
    }

    return await res.json();
}

export async function getPlatforms() {
        const res = await fetch(`backend/platforms`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!res.ok) {
        throw new Error(`Failed to fetch platforms: ${res.status}`);
    }

    return await res.json();
}