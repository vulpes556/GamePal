/** @type {import('next').NextConfig} */
const baseUrl = process.env.BACKEND_URL;
const nextConfig = {
    async rewrites() {
        return [
            {
                source: "/api/:path((?!auth).*)", // Exclude /api/auth
                destination: `${baseUrl}/:path*`,
            },
        ];
    },
};

export default nextConfig;
