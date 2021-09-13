USE CatalogoLivros;
GO

/***********************************************************************************************************************
Criando tabela de livros:
***********************************************************************************************************************/
DROP TABLE IF EXISTS LIVROS
CREATE TABLE LIVROS
(
	LIVROS_ID_INT					INT				NOT NULL IDENTITY(10000000, 1),
	LIVROS_AUTOR_STR				VARCHAR(255)	NOT NULL,
	LIVROS_TITULO_STR				VARCHAR(255)	NOT NULL,
	LIVROS_GENERO_STR				VARCHAR(255)	NOT NULL,
	LIVROS_PRECO_FLOAT				FLOAT(2)		NOT NULL,
	LIVROS_DT_PUBLICACAO_DATE		DATE			NOT NULL,
	LIVROS_DESCRICAO_STR			VARCHAR(255)	NOT NULL,
	LIVROS_DT_CADASTRO_DATETIME		DATETIME		NOT NULL

	--PK
	CONSTRAINT PK_LIVROS_ID_INT PRIMARY KEY(LIVROS_ID_INT)
);


/***********************************************************************************************************************
Criando tabela de logs:
***********************************************************************************************************************/
DROP TABLE IF EXISTS LOGS
CREATE TABLE LOGS
(
	LOGS_ID_INT					INT				NOT NULL	IDENTITY(10000000, 1),
	LOGS_DADOS_STR				VARCHAR(MAX)	NOT NULL,
	LOGS_MENSAGEM_STR			VARCHAR(255)	NOT NULL,
	LOGS_DT_CADASTRO_DATETIME	DATETIME		NOT NULL

	--pK
	CONSTRAINT PK_LOGS_ID_INT PRIMARY KEY(LOGS_ID_INT)
);


/***********************************************************************************************************************
Criando procedure que insere livros na tabela de livros:
***********************************************************************************************************************/
ALTER PROCEDURE [dbo].[uspInserirLivro]
	 @Autor				nvarchar(255)
	,@Titulo			nvarchar(255)
	,@Genero			nvarchar(255)
	,@Preco				nvarchar(255)
	,@DataPublicacao	nvarchar(255)
	,@Descricao			nvarchar(255)

	--uspInserirLivro 'Rowling, J.K.', 'Harry Potter e a Pedra Filosofal', 'Fic��o', '22.50', '2021-08-12', 'Teste'
AS
	BEGIN

		SET NOCOUNT ON;

		DECLARE @Mensagem nvarchar(255);

		IF (SELECT 1 FROM LIVROS WHERE LIVROS_TITULO_STR = @Titulo AND LIVROS_PRECO_FLOAT = @Preco) IS NULL
			BEGIN

				BEGIN TRANSACTION;

					BEGIN TRY

						INSERT INTO LIVROS
						VALUES
						(
							 @Autor
							,@Titulo
							,@Genero
							,CAST(@Preco AS FLOAT(2))
							,CAST(@DataPublicacao AS DATE)
							,@Descricao
							,GETDATE()
						);

						SET @Mensagem = 'Livro cadastrado com sucesso.';

					END TRY

					BEGIN CATCH

						IF @@TRANCOUNT > 0
							ROLLBACK TRANSACTION;

					END CATCH;

				IF @@TRANCOUNT > 0
					COMMIT TRANSACTION;

			END;

		ELSE

			BEGIN

				SET @Mensagem = 'J� existe um livro cadastrado com os dados informados.';

			END;


		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO LOGS
				VALUES
				(
					 CONCAT
					 (
						'Autor: ', @Autor, ';',
						'Titulo: ', @Titulo, ';',
						'Genero: ', @Genero, ';',
						'Preco: ', @Preco, ';',
						'DataPublicacao: ', @DataPublicacao, ';',
						'Descricao: ', @Descricao
					 )
					,@Mensagem
					,GETDATE()
				);

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

	END;
GO