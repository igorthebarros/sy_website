import type { InstagramPost } from "../models/InstagramPost";

const API_BASE_URL = import.meta.env.VITE_API_URL;

export async function getInstagramPosts(): Promise<InstagramPost[]> {
  const response = await fetch(`${API_BASE_URL}/instagram/posts`);
  console.log("Aqui ó: ", response);

  if (!response.ok) {
    throw new Error("Failed to fetch Instagram posts");
  }

  const data = await response.json();

  // Handle both wrapped and direct payloads
  return data.data ?? data;
}