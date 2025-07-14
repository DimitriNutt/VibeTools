import type { ReviewDto } from "./reviewApi";

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5015';

export interface ToolDetailsDto {
  id: string;
  name?: string;
  description?: string;
  category?: string[];
  averageRating?: number;
  isCommunityFavorite?: boolean;
  isHidden?: boolean;
  reviews?: ReviewDto[];
}

export async function getTools(page = 1, pageSize = 10): Promise<ToolDetailsDto[]> {
  const res = await fetch(`${API_URL}/tools?PageNumber=${page}&PageSize=${pageSize}`);
  if (!res.ok) throw new Error('Failed to fetch tools');
  const data = await res.json();
  return data.tools;
}

export async function getToolById(id: string): Promise<ToolDetailsDto> {
  const res = await fetch(`${API_URL}/tools/${id}`);
  if (!res.ok) throw new Error('Failed to fetch tool');
  const data = await res.json();
  return data.tool;
}

export async function createTool(tool: { name: string; category: string[]; description: string }): Promise<{ id: string }> {
  const res = await fetch(`${API_URL}/tools`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(tool),
  });
  if (!res.ok) throw new Error('Failed to create tool');
  return res.json();
}

export async function updateTool(tool: { id: string; name?: string; category?: string[]; description?: string }): Promise<{ isSuccess: boolean }> {
  const res = await fetch(`${API_URL}/tools`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(tool),
  });
  if (!res.ok) throw new Error('Failed to update tool');
  return res.json();
}

export async function searchTools({ name, category, description, page = 1, pageSize = 10 }: { name?: string; category?: string; description?: string; page?: number; pageSize?: number }): Promise<ToolDetailsDto[]> {
  const params = new URLSearchParams();
  if (name) params.append('Name', name);
  if (category) params.append('Category', category);
  if (description) params.append('Description', description);
  params.append('PageNumber', String(page));
  params.append('PageSize', String(pageSize));
  const res = await fetch(`${API_URL}/tools/search?${params.toString()}`);
  if (!res.ok) throw new Error('Failed to search tools');
  const data = await res.json();
  return data.tools;
}