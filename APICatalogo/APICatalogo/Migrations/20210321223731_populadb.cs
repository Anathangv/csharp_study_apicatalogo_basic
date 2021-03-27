using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatalogo.Migrations
{
    public partial class populadb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, " +
                                          "ImagemUrl) " +
                "                   values('Bebidas'," +
                "                          'http://www.teste.com/imagem1.jpg')");

            mb.Sql("Insert into Categorias(Nome, " +
                                          "ImagemUrl) " +
                "                   values('Lanches'," +
                "                          'http://www.teste.com/imagem2.jpg')");

            mb.Sql("Insert into Categorias(Nome, " +
                                          "ImagemUrl) " +
                "                   values('Sobremesas'," +
                "                          'http://www.teste.com/imagem3.jpg')");

            mb.Sql("Insert into Produtos(Nome, " +
                                        "Descricao," +
                                        "Preco," +
                                        "ImagemUrl," +
                                        "Estoque," +
                                        "DataCadastro," +
                                        "CategoriaId) " +
                "                   values('Coca-cola Diet'," +
                "                          'Refrigerante de Cola'," +
                "                          5.45," +
                "                          'http://www.teste.com/cola.jpg'," +
                "                          50," +
                "                           now()," +
                "                           (select CategoriaID from Categorias where Nome = 'Bebidas'))");

            mb.Sql("Insert into Produtos(Nome, " +
                                        "Descricao," +
                                        "Preco," +
                                        "ImagemUrl," +
                                        "Estoque," +
                                        "DataCadastro," +
                                        "CategoriaId) " +
                "                   values('Lanche de Atum'," +
                "                          'Lanche de Atum Com Maionese'," +
                "                          8.50," +
                "                          'http://www.teste.com/lancheatum.jpg'," +
                "                          10," +
                "                           now()," +
                "                           (select CategoriaID from Categorias where Nome = 'Lanches'))");

            mb.Sql("Insert into Produtos(Nome, " +
                                        "Descricao," +
                                        "Preco," +
                                        "ImagemUrl," +
                                        "Estoque," +
                                        "DataCadastro," +
                                        "CategoriaId) " +
            "                   values('Pudim'," +
            "                          'Pudim de chocolate'," +
            "                          6.75," +
            "                          'http://www.teste.com/pudim.jpg'," +
            "                          20," +
            "                          now()," +
            "                          (select CategoriaID from Categorias where Nome = 'Sobremesas'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
            mb.Sql("Delete from Produtos");
        }
    }
}