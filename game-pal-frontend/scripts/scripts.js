export const fetchUserGames = async () => {
    const response = await fetch("/api/user-games", {
        method: "GET",
        headers: {},
    });

    if (!response.ok) {
        console.log(response);
        throw new Error("Something went wrong");
    }
    const result = await response.json();
    console.log("result", result)
    return result;
};