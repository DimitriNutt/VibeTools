import type { ToolDetailsDto } from '../api/toolsApi';
import ToolCard from './ToolCard';

interface ToolGridProps {
    tools: ToolDetailsDto[];
}

export default function ToolGrid({ tools }: ToolGridProps) {
    if (tools.length === 0) {
        return <div>No tools found.</div>;
    }
    return (
        <div
            style={{
                display: 'flex',
                flexWrap: 'wrap',
                gap: '1rem',
                justifyContent: 'center',
            }}
        >
            {tools.map((tool) => (
                <ToolCard key={tool.id} tool={tool} />
            ))}
        </div>
    );
}
