const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5015';

export interface ReviewDto {
  id: string;
  toolId: string;
  rating: number;
  comment?: string;
  createdAt: string;
  reviewerName?: string;
}

export interface CreateReviewRequest {
  rating: number;
  comment?: string;
  toolId: string;
}

export async function createReview(review: CreateReviewRequest): Promise<{ id: string }> {
  const res = await fetch(`${API_URL}/reviews`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(review),
  });
  if (!res.ok) throw new Error('Failed to create review');
  return res.json();
}
