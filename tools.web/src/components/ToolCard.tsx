import './../index.css';
import type { ToolDetailsDto } from '../api/toolsApi';

interface ToolCardProps {
    tool: ToolDetailsDto;
}

export default function ToolCard({ tool }: ToolCardProps) {
    return (
        <a href={`/tools/${tool.id}`}>
            <div className="card">
                <div className="title">
                    {tool.name}
                    {tool.isCommunityFavorite && (
                        <span style={{ color: '#fbc02d', marginLeft: 8, fontSize: 28, verticalAlign: 'super' }}>★</span>
                    )}
                </div>
                <div className="description">{tool.description}</div>
                <div className="rating">
                    {tool.averageRating !== undefined ? `⭐ ${tool.averageRating.toFixed(1)}` : 'No ratings yet'}
                </div>

                <div className="categories">
                    {tool.category?.join(', ') || 'Uncategorized'}
                </div>

                {/* <div className="actions">
                <a href={`/tools/${tool.id}`}>Details</a>
            </div> */}
            </div>
        </a>
    );
}
