# Dialog System

This Unity project contains a prototype dialog system. When starting an interaction a dialog box appears and the written texts
are shown in the order expected in that box.

The box consists of an image, which is not required. The character's name and the content of the dialogue.

Clicking on the dialog box moves on to the next text. When the full dialog ends, the box disappears and can be triggered again.

## scripts

### Dialog Manager

- It is the script responsible for building what will appear in the dialog box. It receives the information of the character that speaks, in addition to the text to be placed.

- It has the reference of the Dialog box components.

- Activates the "animation" of the dialog that appears as if it were being typed (Typewriter Effect).

- Shows the next text when clicking on the Dialog box

### Dialog Trigger

- Main class that activates the Dialog box.

- Contains the text, characters and order for the text to be displayed using the Dialog Insert class.

- At the end of the dialogue, specific actions (deliver item, change scene, ...) occur.

### Dialog Insert

- Serialized class that contains the properties to make the text available as:
- Text language 1
- Text language 2
- "Actor"

### Actor

- Scriptable Object

- Serves to store the properties of a character that is generally repeated
  
### Simple Interaction

- Example of an interaction. It inherits from Dialog Trigger, being able to have actions different from the basic ones.

##


# Sistema de Diálogo

Esse projeto da Unity contem um protótipo de um sistema de diálogo. Ao iniciar uma interação uma caixa de diálogo aparece e os textos escritos 
são mostrados na ordem esperada nessa caixa. 

A caixa consiste em uma imagem (avatar da personagem falando), que não é necessária. O nome da personagem e o contúedo do diálogo. 

Clicando na caixa de diálogo, passa-se ao próximo texto. Quando o diálogo completo termina, a caixa desaparece e pode ser acionada novamente.

## Scripts

### Dialog Manager

- É o script responsável pela construção do que irá aparecer na caixa de diálogo. Recebe as informações da personagem que fala, além do texto a ser colocado.

- Tem a referência dos componentes da caixa de Diálogo.

- Ativa a "animação" do diálogo que aparece como se estivesse sendo digitado (Typewriter Effect).

- Mostra o próximo texto quando se clica na caixa de Diálogo

### Dialog Trigger

- Classe principal que ativa a caixa de Diálogo.

- Contém o texto, personagens e ordem para o texto ser mostrado utilizando a classe Dialog Insert.

- Ao fim do diálogo da a possibilidade de ações específicas (entregar item, trocar de cena, ...) ocorrerem.

### Dialog Insert

- Classe serializada que contém as propriedades para disponibilizar o texto como:
- Texto linguagem 1
- Texto linguagem 2
- "Ator"

### Actor

- Scriptable Object

- Serve para guardar as propriedades de uma personagem que em geral se repete
  
### Simple Interaction

- Exemplo de uma interação. Herda de Dialog Trigger, podendo ter ações diferentes das básicas.
