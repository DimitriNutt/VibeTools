import type { ToolDetailsDto } from '../api/toolsApi';

interface ToolInfoProps {
    tool: ToolDetailsDto;
}

export default function ToolInfo({ tool }: ToolInfoProps) {
    return (
        <div style={{ maxWidth: 600, margin: '2rem auto' }}>
            <h2>
                {tool.name}
                {tool.isCommunityFavorite && <span style={{ color: '#fbc02d', marginLeft: 8, fontSize: 28, verticalAlign: 'super' }}>★</span>}
            </h2>
            <div><strong>Categories:</strong> {tool.category?.join(', ') || 'Uncategorized'}</div>
            <div style={{ margin: '1rem 0' }}>{tool.description}</div>
            <div><strong>Average Rating:</strong> {tool.averageRating !== undefined ? tool.averageRating.toFixed(1) + ' ⭐' : 'N/A'}</div>
            <div>
                <strong>Community Favorite:</strong> {tool.isCommunityFavorite ? 'Yes' : 'No'}
            </div>
            <div><strong>Hidden:</strong> {tool.isHidden ? 'Yes' : 'No'}</div>
        </div>
    );
}
