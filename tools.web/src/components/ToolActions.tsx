import { Link } from 'react-router-dom';

interface ToolActionsProps {
    toolId: string;
}

export default function ToolActions({ toolId }: ToolActionsProps) {
    return (
        <div style={{ marginTop: '1rem', display: 'flex', gap: '1rem' }}>
            <Link to={`/tools/${toolId}/edit`}>Edit Tool Details</Link>
        </div>
    );
}
