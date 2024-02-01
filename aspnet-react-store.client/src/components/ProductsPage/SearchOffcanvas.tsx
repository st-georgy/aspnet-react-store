import { useEffect, useState } from 'react';
import { Button, Form, InputGroup, Offcanvas } from 'react-bootstrap';

interface SearchOffcanvasProps {
  show: boolean;
  handleClose: () => void;
  searchText?: string;
}

const SearchForm = ({ value, onChange, onClick }: any) => (
  <Form style={{ width: '70%' }}>
    <InputGroup className='mb-3'>
      <InputGroup.Text>Название товара</InputGroup.Text>
      <Form.Control value={value} onChange={onChange} />
    </InputGroup>
    <Button
      variant='outline-secondary'
      onClick={onClick}
      className='float-end'
      style={{ width: '15%' }}
    >
      Искать
    </Button>
  </Form>
);

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
    if (searchInputValue && searchInputValue.trim() !== '')
      window.location.href =
        '/products?searchText=' + encodeURIComponent(searchInputValue.trim());
  };

  const handleHide = () => {
    handleClose();
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleSearch();
    }
  };

  useEffect(() => {
    if (searchText) setSearchInputValue(searchText);
  }, []);

  return (
    <Offcanvas
      show={show}
      onHide={handleHide}
      placement='top'
      onKeyDown={handleKeyDown}
    >
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Поиск товаров</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body className='d-flex align-items-center justify-content-center'>
        <SearchForm
          value={searchInputValue}
          onChange={handleInputChange}
          onClick={handleSearch}
        />
      </Offcanvas.Body>
    </Offcanvas>
  );
}
