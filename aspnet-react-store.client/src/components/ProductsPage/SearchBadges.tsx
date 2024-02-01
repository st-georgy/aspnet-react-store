import { Badge, Button } from 'react-bootstrap';
import { AiOutlineClose } from 'react-icons/ai';

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
      <Badge bg='secondary'>
        <span>'{searchText}' </span>
        <Button
          variant='link'
          onClick={handleRemoveSearchText}
          style={{
            borderRadius: '50%',
            padding: 0,
            backgroundColor: 'transparent',
          }}
        >
          <AiOutlineClose
            size={25}
            color='white'
            style={{ paddingBottom: '5px' }}
          />
        </Button>
      </Badge>
    </h3>
  );
}
