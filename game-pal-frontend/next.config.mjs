/** @type {import('next').NextConfig} */
const baseUrl = process.env.BACKEND_URL;

const nextConfig = {
  images: {
    domains: ['devimages-cdn.apple.com'], // temporary external image host
  },
  async rewrites() {
    return [
      {
        source: "/backend/:path*",
        destination: `${baseUrl}/:path*`,
      },
    ];
  },
};

export default nextConfig;
