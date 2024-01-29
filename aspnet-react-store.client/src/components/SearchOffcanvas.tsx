import { useEffect, useState } from 'react';
import { Button, FormControl, InputGroup, Offcanvas } from 'react-bootstrap';

interface SearchOffcanvasProps {
  show: boolean;
  handleClose: () => void;
  searchText?: string;
}

export default function SearchOffcanvas({
  show,
  handleClose,
  searchText,
}: SearchOffcanvasProps) {
  const [searchInputValue, setSearchInputValue] = useState('');

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchInputValue(e.target.value);
  };

  const handleSearch = () => {
    console.log('Искать:', searchInputValue);
  };

  const handleHide = () => {
    setSearchInputValue('');
    handleClose();
  };

  useEffect(() => {
    if (searchText) setSearchInputValue(searchText);
  }, []);

  return (
    <Offcanvas show={show} onHide={handleHide} placement='top'>
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Поиск товаров</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body>
        <InputGroup className='mb-3'>
          <FormControl
            placeholder='Введите название товара'
            aria-label='Введите название товара'
            aria-describedby='basic-addon2'
            value={searchInputValue}
            onChange={handleInputChange}
          />
          <Button variant='outline-secondary' onClick={handleSearch}>
            Искать
          </Button>
        </InputGroup>
      </Offcanvas.Body>
    </Offcanvas>
  );
}
