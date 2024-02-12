import { Chip } from '@mui/material';

interface SearchBadgesProps {
  searchText: string;
  onRemoveSearchText: () => void;
}

export default function SearchBadges({
  searchText,
  onRemoveSearchText,
}: SearchBadgesProps) {
  const handleRemoveSearchText = () => {
    onRemoveSearchText();
  };

  return (
    <h3 style={{ marginBottom: '5rem' }}>
      Поиск товаров{' '}
      <Chip label={`'${searchText}'`} onDelete={handleRemoveSearchText} />
    </h3>
  );
}
