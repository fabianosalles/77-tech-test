<template>    
    <BookList @show-new-book-form="onShowNewBookForm" ref="bookList"></BookList>
    <AddBookForm @new-book-added="onNewBookAdded" ref="newBookForm"></AddBookForm>
</template>

<script>
import BookList from '@/components/BookList.vue';
import AddBookForm from '@/components/AddBookForm.vue';

export default {
  name: 'BookView',
  components: {
    BookList,
    AddBookForm
  },
  methods: {
    onShowNewBookForm() { this.$refs.newBookForm.showModal() },
    onNewBookAdded() { this.$refs.bookList.reload() }
  }
}
</script>